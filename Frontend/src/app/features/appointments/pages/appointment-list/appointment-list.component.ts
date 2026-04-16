import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ConfirmedAppointment } from '../../models/appointment.model';
import { formatAppointmentDate, formatAppointmentTimeRange } from '../../../../shared/utils/appointment-format.util';
import { AppointmentApiService } from '../../services/appointment.api';

@Component({
  selector: 'app-appointment-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './appointment-list.component.html',
  styleUrl: './appointment-list.component.css'
})
export class AppointmentListComponent implements OnInit {
  private readonly appointmentApi = inject(AppointmentApiService);
  private readonly router = inject(Router);
  private readonly destroyRef = inject(DestroyRef);

  protected readonly data = signal<ConfirmedAppointment[]>([]);
  protected readonly loading = signal(true);
  protected readonly error = signal('');
  protected readonly formatDate = formatAppointmentDate;
  protected readonly formatTimeRange = formatAppointmentTimeRange;

  ngOnInit(): void {
    this.appointmentApi.getAppointments()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (v) => { this.data.set(v); this.loading.set(false); },
        error: () => { this.error.set('Unable to load appointments.'); this.loading.set(false); }
      });
  }

  protected goBack() { this.router.navigate(['/mode']); }
}
