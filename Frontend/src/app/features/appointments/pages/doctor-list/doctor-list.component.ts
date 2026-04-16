import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Doctor } from '../../models/doctor.model';
import { BookingFlowService } from '../../services/booking-flow.service';
import { DoctorApiService } from '../../services/doctor.api';

@Component({
  selector: 'app-doctor-list',
  standalone: true,
  templateUrl: './doctor-list.component.html',
  styleUrl: './doctor-list.component.css'
})
export class DoctorListComponent implements OnInit {
  private readonly doctorApi = inject(DoctorApiService);
  private readonly bookingFlow = inject(BookingFlowService);
  private readonly router = inject(Router);
  private readonly destroyRef = inject(DestroyRef);

  protected readonly selectedMode = this.bookingFlow.mode;
  protected readonly selectedSpecialty = this.bookingFlow.specialty;
  protected readonly data = signal<Doctor[]>([]);
  protected readonly loading = signal(true);
  protected readonly error = signal('');

  ngOnInit(): void {
    const mode = this.bookingFlow.mode();
    const specialty = this.bookingFlow.specialty();
    if (!mode) { this.router.navigate(['/mode']); return; }
    if (!specialty) { this.router.navigate(['/specialties']); return; }

    this.doctorApi.getDoctors(mode, specialty.specialtyId)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (v) => { this.data.set(v); this.loading.set(false); },
        error: () => { this.error.set('Unable to load doctors.'); this.loading.set(false); }
      });
  }

  protected viewSlots(d: Doctor) { this.bookingFlow.setDoctor(d); this.router.navigate(['/slots']); }
  protected goBack() { this.router.navigate(['/specialties']); }
}
