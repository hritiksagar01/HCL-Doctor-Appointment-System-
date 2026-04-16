import { Doctor } from './doctor.model';
import { Slot } from './slot.model';
import { Specialty } from './specialty.model';

export type AppointmentMode = 'Online' | 'Offline';

export interface Appointment {
  appointmentId: number;
  userId: number;
  doctorId: number;
  slotId: number;
  mode: AppointmentMode;
  status: string;
  bookingDate: string;
  meetingLink: string | null;
  clinicAddress: string | null;
}

export interface AppointmentDraft {
  mode: AppointmentMode | null;
  specialty: Specialty | null;
  doctor: Doctor | null;
  slot: Slot | null;
}

export interface CreateAppointmentRequest {
  userId: number;
  doctor: Doctor;
  slot: Slot;
  mode: AppointmentMode;
}

export interface ConfirmedAppointment extends Appointment {
  doctorName: string;
  specialtyName: string;
  slotDate: string;
  startTime: string;
  endTime: string;
}
