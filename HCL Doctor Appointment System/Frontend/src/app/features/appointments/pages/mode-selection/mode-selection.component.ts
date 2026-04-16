import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { AppointmentMode } from '../../models/appointment.model';
import { BookingFlowService } from '../../services/booking-flow.service';

@Component({
  selector: 'app-mode-selection',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mode-selection.component.html',
  styleUrl: './mode-selection.component.css'
})
export class ModeSelectionComponent {
  private readonly bookingFlowService = inject(BookingFlowService);
  private readonly router = inject(Router);

  protected selectMode(mode: AppointmentMode): void {
    this.bookingFlowService.setMode(mode);
    void this.router.navigate(['/specialties']);
  }
}
