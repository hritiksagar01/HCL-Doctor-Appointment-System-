import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { finalize } from 'rxjs';
import { AuthService } from '../../../../core/services/auth.service';
import { formatAppointmentDate, formatAppointmentTimeRange } from '../../../../shared/utils/appointment-format.util';
import { AppointmentApiService } from '../../services/appointment.api';
import { BookingFlowService } from '../../services/booking-flow.service';

@Component({
  selector: 'app-confirmation',
  standalone: true,
  templateUrl: './confirmation.component.html',
  styleUrl: './confirmation.component.css'
})
export class ConfirmationComponent implements OnInit {
  private readonly appointmentApi = inject(AppointmentApiService);
  private readonly bookingFlow = inject(BookingFlowService);
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly destroyRef = inject(DestroyRef);

  protected readonly doctor = this.bookingFlow.doctor;
  protected readonly slot = this.bookingFlow.slot;
  protected readonly mode = this.bookingFlow.mode;
  protected readonly formatDate = formatAppointmentDate;
  protected readonly formatTimeRange = formatAppointmentTimeRange;
  protected readonly isSubmitting = signal(false);
  protected readonly errorMessage = signal('');

  ngOnInit(): void {
    if (!this.bookingFlow.mode()) this.router.navigate(['/mode']);
    else if (!this.bookingFlow.doctor()) this.router.navigate(['/doctors']);
    else if (!this.bookingFlow.slot()) this.router.navigate(['/slots']);
  }

  protected confirmAppointment(): void {
    const user = this.authService.currentUser();
    const doctor = this.bookingFlow.doctor();
    const slot = this.bookingFlow.slot();
    const mode = this.bookingFlow.mode();

    if (!user || !doctor || !slot || !mode) {
      this.errorMessage.set('Missing booking details. Please restart the flow.');
      return;
    }

    this.isSubmitting.set(true);
    this.errorMessage.set('');

    this.appointmentApi
      .createAppointment({ userId: user.userId, doctor, slot, mode })
      .pipe(finalize(() => this.isSubmitting.set(false)), takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (apt) => { this.bookingFlow.setConfirmation(apt); this.router.navigate(['/success']); },
        error: () => this.errorMessage.set('Unable to confirm the appointment right now.')
      });
  }

  protected goBack() { this.router.navigate(['/slots']); }
}
