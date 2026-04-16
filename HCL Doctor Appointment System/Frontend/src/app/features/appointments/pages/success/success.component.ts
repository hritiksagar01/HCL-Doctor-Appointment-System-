import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { formatAppointmentDate, formatAppointmentTimeRange } from '../../../../shared/utils/appointment-format.util';
import { BookingFlowService } from '../../services/booking-flow.service';

@Component({
  selector: 'app-success',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './success.component.html',
  styleUrl: './success.component.css'
})
export class SuccessComponent implements OnInit {
  private readonly bookingFlow = inject(BookingFlowService);
  private readonly router = inject(Router);

  protected readonly confirmation = this.bookingFlow.confirmation;
  protected readonly formatDate = formatAppointmentDate;
  protected readonly formatTimeRange = formatAppointmentTimeRange;

  ngOnInit(): void {
    if (!this.bookingFlow.confirmation()) this.router.navigate(['/confirm']);
  }

  protected bookAnother() { this.bookingFlow.resetBooking(); this.router.navigate(['/mode']); }
  protected viewAppointments() { this.bookingFlow.resetBooking(); this.router.navigate(['/appointments']); }
}
