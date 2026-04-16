import { computed, Injectable, signal } from '@angular/core';
import { AuthResponse, AuthUser } from '../../features/auth/models/auth.model';

interface AuthSession {
  token: string;
  user: AuthUser;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly storageKey = 'doctor-booking-session';
  private readonly sessionState = signal<AuthSession | null>(this.readSession());

  readonly currentUser = computed(() => this.sessionState()?.user ?? null);
  readonly token = computed(() => this.sessionState()?.token ?? null);
  readonly isAuthenticated = computed(() => this.token() !== null);

  setSession(response: AuthResponse): void {
    const session: AuthSession = {
      token: response.token,
      user: response.user
    };

    this.sessionState.set(session);
    localStorage.setItem(this.storageKey, JSON.stringify(session));
  }

  logout(): void {
    this.sessionState.set(null);
    localStorage.removeItem(this.storageKey);
  }

  private readSession(): AuthSession | null {
    const savedSession = localStorage.getItem(this.storageKey);

    if (!savedSession) {
      return null;
    }

    try {
      return JSON.parse(savedSession) as AuthSession;
    } catch {
      localStorage.removeItem(this.storageKey);
      return null;
    }
  }
}
