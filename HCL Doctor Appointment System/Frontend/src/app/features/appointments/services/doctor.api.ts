import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { AppointmentMode } from '../models/appointment.model';
import { Doctor } from '../models/doctor.model';
import { Specialty } from '../models/specialty.model';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DoctorApiService {
  private readonly http = inject(HttpClient);

  getSpecialties(): Observable<Specialty[]> {
    return this.http.get<Specialty[]>(`${environment.apiUrl}/specialties`).pipe(
      map(s => s.sort((a, b) => a.specialtyName.localeCompare(b.specialtyName)))
    );
  }

  getDoctors(mode: AppointmentMode, specialtyId: number): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${environment.apiUrl}/doctors`, {
      params: { mode, specialtyId: specialtyId.toString() }
    }).pipe(
      map(doctors => doctors.sort((a, b) => b.experience - a.experience))
    );
  }
}
