import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  return next(req).pipe(
    catchError((error) => {
      if (error.status === 400) {
        alert(error.message|| error.error);
      }
      if (error.status === 401) {
        alert(error.message|| error.error);
      }
      if (error.status === 404) {
        router.navigate(['/not-found']);
      }
      if (error.status === 500) {
        router.navigate(['/server-error']);
      }
      return throwError(()=> error);
    })

  );
};
