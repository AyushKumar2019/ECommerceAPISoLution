import { HttpClient } from '@angular/common/http';
import { Component, inject } from '@angular/core';
import { MatButton } from '@angular/material/button';
import { baseUrl } from '../../shared/Constants/Constant';

@Component({
  selector: 'app-test-error',
  imports: [
    MatButton
  ],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.scss'
})
export class TestErrorComponent {
  private http = inject(HttpClient);

  get404Error(){
    this.http.get(baseUrl + 'buggy/notfound').subscribe({
      next: response => {
        console.log(response);
      },
      error: err => {
        console.log(err);
      }
    })
  }
  get400Error(){
    this.http.get(baseUrl + 'buggy/badrequest').subscribe({
      next: response => {
        console.log(response);
      },
      error: err => {
        console.log(err);
      }
    })
  }
  get500Error(){
    this.http.get(baseUrl + 'buggy/internalerror').subscribe({
      next: response => {
        console.log(response);
      },
      error: err => {
        console.log(err);
      }
    })
  }
  get401Error(){
    this.http.get(baseUrl + 'buggy/unauthorized').subscribe({
      next: response => {
        console.log(response);
      },
      error: err => {
        console.log(err);
      }
    })
  }
  get400ValidationError(){
    this.http.post(baseUrl + 'buggy/validationerror',{}).subscribe({
      next: response => {
        console.log(response);
      },
      error: err => {
        console.log(err);
      }
    })
  }


}
