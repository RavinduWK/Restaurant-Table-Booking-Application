import { Injectable } from '@angular/core';
import { LoginService } from '../service/login.service';
import { Subscription } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private currentUserRole: string | undefined;
  private subscription: Subscription;

  constructor(private loginService: LoginService) {
    this.subscription = this.loginService.claims$.subscribe((claims) => {
      const isAdminClaim = claims.find(
        (claim) => claim.claim === 'extension_EmployeeRole'
      );
      if (isAdminClaim) {
        this.currentUserRole = isAdminClaim.value;
        console.log(this.currentUserRole);
      }
    });
  }

  isAdminOrSuperAdmin(): boolean {
    return (
      this.currentUserRole === 'Admin' || this.currentUserRole === 'SuperAdmin'
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
