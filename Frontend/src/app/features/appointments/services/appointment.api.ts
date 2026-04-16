import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  ConfirmedAppointment,
  CreateAppointmentRequest
} from '../models/appointment.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AppointmentApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/appointments`;

  createAppointment(request: CreateAppointmentRequest): Observable<ConfirmedAppointment> {
    return this.http.post<ConfirmedAppointment>(this.baseUrl, {
      doctorId: request.doctor.doctorId,
      slotId: request.slot.slotId,
      mode: request.mode
    });
  }

  getAppointments(): Observable<ConfirmedAppointment[]> {
    return this.http.get<ConfirmedAppointment[]>(`${this.baseUrl}/my`);
  }
}
