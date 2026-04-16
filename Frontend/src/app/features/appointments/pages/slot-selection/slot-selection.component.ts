import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { formatAppointmentDate, formatAppointmentTimeRange } from '../../../../shared/utils/appointment-format.util';
import { Slot } from '../../models/slot.model';
import { BookingFlowService } from '../../services/booking-flow.service';
import { SlotApiService } from '../../services/slot.api';

@Component({
  selector: 'app-slot-selection',
  standalone: true,
  templateUrl: './slot-selection.component.html',
  styleUrl: './slot-selection.component.css'
})
export class SlotSelectionComponent implements OnInit {
  private readonly slotApi = inject(SlotApiService);
  private readonly bookingFlow = inject(BookingFlowService);
  private readonly router = inject(Router);
  private readonly destroyRef = inject(DestroyRef);

  protected readonly doctor = this.bookingFlow.doctor;
  protected readonly mode = this.bookingFlow.mode;
  protected readonly data = signal<Slot[]>([]);
  protected readonly loading = signal(true);
  protected readonly error = signal('');
  protected readonly formatDate = formatAppointmentDate;
  protected readonly formatTimeRange = formatAppointmentTimeRange;

  private readonly MONTHS = ['JAN','FEB','MAR','APR','MAY','JUN','JUL','AUG','SEP','OCT','NOV','DEC'];

  ngOnInit(): void {
    const doc = this.bookingFlow.doctor();
    if (!this.bookingFlow.mode()) { this.router.navigate(['/mode']); return; }
    if (!doc) { this.router.navigate(['/doctors']); return; }

    this.slotApi.getSlotsByDoctor(doc.doctorId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (v) => { this.data.set(v); this.loading.set(false); },
        error: () => { this.error.set('Unable to load slots.'); this.loading.set(false); }
      });
  }

  protected selectSlot(s: Slot) { if (s.isBooked) return; this.bookingFlow.setSlot(s); this.router.navigate(['/confirm']); }
  protected goBack() { this.router.navigate(['/doctors']); }
  protected getMonth(d: string) { return this.MONTHS[new Date(d).getMonth()] ?? ''; }
  protected getDay(d: string) { return String(new Date(d).getDate()).padStart(2, '0'); }
}
