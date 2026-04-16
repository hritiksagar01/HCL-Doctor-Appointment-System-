import { AppointmentMode } from './appointment.model';

export interface Doctor {
  doctorId: number;
  doctorName: string;
  specialtyId: number;
  specialtyName: string;
  experience: number;
  fees: number;
  mode: AppointmentMode;
  email: string;
  clinicAddress: string;
}
