import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { ConfirmationComponent } from './features/appointments/pages/confirmation/confirmation.component';
import { DoctorListComponent } from './features/appointments/pages/doctor-list/doctor-list.component';
import { ModeSelectionComponent } from './features/appointments/pages/mode-selection/mode-selection.component';
import { SlotSelectionComponent } from './features/appointments/pages/slot-selection/slot-selection.component';
import { SpecialtySelectionComponent } from './features/appointments/pages/specialty-selection/specialty-selection.component';
import { SuccessComponent } from './features/appointments/pages/success/success.component';
import { AppointmentListComponent } from './features/appointments/pages/appointment-list/appointment-list.component';
import { LoginComponent } from './features/auth/pages/login/login.component';

export const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    redirectTo: 'login'
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'mode',
    component: ModeSelectionComponent,
    canActivate: [authGuard]
  },
  {
    path: 'specialties',
    component: SpecialtySelectionComponent,
    canActivate: [authGuard]
  },
  {
    path: 'doctors',
    component: DoctorListComponent,
    canActivate: [authGuard]
  },
  {
    path: 'slots',
    component: SlotSelectionComponent,
    canActivate: [authGuard]
  },
  {
    path: 'confirm',
    component: ConfirmationComponent,
    canActivate: [authGuard]
  },
  {
    path: 'success',
    component: SuccessComponent,
    canActivate: [authGuard]
  },
  {
    path: 'appointments',
    component: AppointmentListComponent,
    canActivate: [authGuard]
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];
