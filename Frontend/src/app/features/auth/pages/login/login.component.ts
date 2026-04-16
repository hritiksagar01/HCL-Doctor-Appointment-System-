import { CommonModule } from '@angular/common';
import { Component, DestroyRef, inject, OnInit, signal } from '@angular/core';
import { NonNullableFormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { finalize } from 'rxjs';
import { AuthService } from '../../../../core/services/auth.service';
import { AuthApiService } from '../../services/auth.api';

type AuthTab = 'login' | 'register';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  private readonly fb = inject(NonNullableFormBuilder);
  private readonly authApi = inject(AuthApiService);
  private readonly authService = inject(AuthService);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);
  private readonly destroyRef = inject(DestroyRef);

  protected readonly activeTab = signal<AuthTab>('login');
  protected readonly isSubmitting = signal(false);
  protected readonly errorMessage = signal('');
  protected readonly demoEmail = 'demo@stoiccare.com';
  protected readonly demoPassword = 'demo12345';

  protected readonly loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  protected readonly registerForm = this.fb.group({
    username: ['', [Validators.required, Validators.minLength(2)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });

  ngOnInit(): void {
    if (this.authService.isAuthenticated()) this.router.navigate(['/mode']);
  }

  protected switchTab(tab: AuthTab) { this.activeTab.set(tab); this.errorMessage.set(''); }

  protected useDemoCredentials() {
    this.switchTab('login');
    this.loginForm.setValue({ email: this.demoEmail, password: this.demoPassword });
  }

  protected submitLogin(): void {
    if (this.loginForm.invalid) { this.loginForm.markAllAsTouched(); return; }
    this.isSubmitting.set(true);
    this.errorMessage.set('');

    this.authApi.login(this.loginForm.getRawValue())
      .pipe(finalize(() => this.isSubmitting.set(false)), takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res) => {
          this.authService.setSession(res);
          this.router.navigateByUrl(this.route.snapshot.queryParamMap.get('redirectUrl') ?? '/mode');
        },
        error: (err: Error) => this.errorMessage.set(err.message)
      });
  }

  protected submitRegister(): void {
    if (this.registerForm.invalid) { this.registerForm.markAllAsTouched(); return; }
    this.isSubmitting.set(true);
    this.errorMessage.set('');

    this.authApi.register(this.registerForm.getRawValue())
      .pipe(finalize(() => this.isSubmitting.set(false)), takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res) => { this.authService.setSession(res); this.router.navigate(['/mode']); },
        error: (err: Error) => this.errorMessage.set(err.message)
      });
  }

  protected hasError(form: 'login' | 'register', control: string, error: string): boolean {
    const group = form === 'login' ? this.loginForm : this.registerForm;
    const ctrl = group.controls[control as keyof typeof group.controls];
    return Boolean(ctrl?.touched && ctrl.hasError(error));
  }
}
