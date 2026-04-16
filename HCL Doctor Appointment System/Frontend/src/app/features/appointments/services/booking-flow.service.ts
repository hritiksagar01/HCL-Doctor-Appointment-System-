import { Injectable, computed, signal } from '@angular/core';
import {
  AppointmentDraft,
  ConfirmedAppointment
} from '../models/appointment.model';
import { Doctor } from '../models/doctor.model';
import { Slot } from '../models/slot.model';
import { Specialty } from '../models/specialty.model';

interface BookingFlowState extends AppointmentDraft {
  confirmation: ConfirmedAppointment | null;
}

const initialState: BookingFlowState = {
  mode: null,
  specialty: null,
  doctor: null,
  slot: null,
  confirmation: null
};

@Injectable({
  providedIn: 'root'
})
export class BookingFlowService {
  private readonly storageKey = 'doctor-booking-flow';
  private readonly state = signal<BookingFlowState>(this.readState());

  readonly draft = computed(() => ({
    mode: this.state().mode,
    specialty: this.state().specialty,
    doctor: this.state().doctor,
    slot: this.state().slot
  }));
  readonly mode = computed(() => this.state().mode);
  readonly specialty = computed(() => this.state().specialty);
  readonly doctor = computed(() => this.state().doctor);
  readonly slot = computed(() => this.state().slot);
  readonly confirmation = computed(() => this.state().confirmation);

  setMode(mode: BookingFlowState['mode']): void {
    this.updateState({
      mode,
      specialty: null,
      doctor: null,
      slot: null,
      confirmation: null
    });
  }

  setSpecialty(specialty: Specialty): void {
    this.updateState({
      specialty,
      doctor: null,
      slot: null,
      confirmation: null
    });
  }

  setDoctor(doctor: Doctor): void {
    this.updateState({
      doctor,
      slot: null,
      confirmation: null
    });
  }

  setSlot(slot: Slot): void {
    this.updateState({
      slot,
      confirmation: null
    });
  }

  setConfirmation(confirmation: ConfirmedAppointment): void {
    this.updateState({ confirmation });
  }

  resetBooking(): void {
    this.updateState({
      specialty: null,
      doctor: null,
      slot: null,
      confirmation: null
    });
  }

  clearFlow(): void {
    this.state.set(initialState);
    localStorage.removeItem(this.storageKey);
  }

  private updateState(partialState: Partial<BookingFlowState>): void {
    const nextState = {
      ...this.state(),
      ...partialState
    };

    this.state.set(nextState);
    localStorage.setItem(this.storageKey, JSON.stringify(nextState));
  }

  private readState(): BookingFlowState {
    const savedState = localStorage.getItem(this.storageKey);

    if (!savedState) {
      return initialState;
    }

    try {
      return {
        ...initialState,
        ...(JSON.parse(savedState) as Partial<BookingFlowState>)
      };
    } catch {
      localStorage.removeItem(this.storageKey);
      return initialState;
    }
  }
}
