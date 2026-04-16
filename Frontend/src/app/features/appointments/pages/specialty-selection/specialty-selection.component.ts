import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Specialty } from '../../models/specialty.model';
import { BookingFlowService } from '../../services/booking-flow.service';
import { DoctorApiService } from '../../services/doctor.api';

const ICON: Record<string, string> = {
  cardiology: 'favorite', dermatology: 'face', pediatrics: 'child_care',
  neurology: 'psychology', orthopedics: 'skeleton', psychiatry: 'self_improvement',
  'general medicine': 'medical_services',
};

@Component({
  selector: 'app-specialty-selection',
  standalone: true,
  templateUrl: './specialty-selection.component.html',
  styleUrl: './specialty-selection.component.css'
})
export class SpecialtySelectionComponent implements OnInit {
  private readonly doctorApi = inject(DoctorApiService);
  private readonly bookingFlow = inject(BookingFlowService);
  private readonly router = inject(Router);
  private readonly destroyRef = inject(DestroyRef);

  protected readonly data = signal<Specialty[]>([]);
  protected readonly loading = signal(true);
  protected readonly error = signal('');

  ngOnInit(): void {
    if (!this.bookingFlow.mode()) { this.router.navigate(['/mode']); return; }

    this.doctorApi.getSpecialties()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (v) => { this.data.set(v); this.loading.set(false); },
        error: () => { this.error.set('Unable to load specialties.'); this.loading.set(false); }
      });
  }

  protected select(s: Specialty) { this.bookingFlow.setSpecialty(s); this.router.navigate(['/doctors']); }
  protected goBack() { this.router.navigate(['/mode']); }
  protected icon(n: string) { return ICON[n.toLowerCase().trim()] ?? 'medical_services'; }
  protected variant(i: number) { return ['primary', 'tertiary', 'secondary'][i % 3]; }
}
