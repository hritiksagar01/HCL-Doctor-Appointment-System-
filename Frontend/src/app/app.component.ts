import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AuthService } from './core/services/auth.service';
import { BookingFlowService } from './features/appointments/services/booking-flow.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  protected readonly authService = inject(AuthService);
  private readonly bookingFlowService = inject(BookingFlowService);
  private readonly router = inject(Router);

  protected signOut(): void {
    this.authService.logout();
    this.bookingFlowService.clearFlow();
    void this.router.navigate(['/login']);
  }
}
