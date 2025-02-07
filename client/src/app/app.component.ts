import { Component , inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from './layout/header/header.component';
import { HttpClient } from '@angular/common/http';
import { Product } from './shared/models/product';
import { Pagination } from './shared/models/Pagination';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  baseUrl = 'https://localhost:7010/api/';
  private http = inject(HttpClient);
  title = 'client';
  products:Product[]=[];
  ngOnInit(): void {
    this.http.get<Pagination<Product>>(`${this.baseUrl}`+ 'products').subscribe({
      next: data =>{ this.products = data.data;  console.log( data)},
      error: err => console.error(err),
      complete: () => console.log('done')
    });

  }
}
