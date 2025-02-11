import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { MatCardModule } from '@angular/material/card';
import { MatDialog } from '@angular/material/dialog';
import { ProductItemComponent } from "./product-item/product-item.component";
import { FiltersDialogComponent } from '../filters-dialog/filters-dialog.component';
import { min } from 'rxjs';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import { MatMenuModule, MatMenuTrigger } from '@angular/material/menu';
import { shopParams } from '../../shared/models/shopParams';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { Pagination } from '../../shared/models/Pagination';
@Component({
  selector: 'app-shop',
  standalone: true,
  imports: [MatCardModule,
    ProductItemComponent,
    MatButton,
    MatIcon,
    MatMenuModule,
    MatMenuTrigger,
    MatPaginatorModule,
    MatListOption,
    MatSelectionList
    ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
handlePageEvent(event: PageEvent) {
this.shopParams.pageNumber = event.pageIndex + 1;
this.shopParams.pageSize = event.pageSize;
this.getProducts();
}

  private shopService = inject(ShopService);
  private dialogService = inject(MatDialog);
  products?: Pagination<Product>;
  pageSizeOptions = [5,10,15,20];
  shopParams:shopParams = new shopParams();
  sortOptions = [
    {name: 'Alphabetical', value: 'name'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'}
  ];
  ngOnInit(): void {
    this.initializeshop();
  }


  initializeshop(){
    this.shopService.getBrands();
    this.shopService.getTypes();
    this.getProducts();
  }
  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe({
      next: response => {
        console.log(response);
          this.products = response;
      },
      error: err => console.error(err)
    });

  }

  onSortChange(event: MatSelectionListChange) {

    const selectedOption = event.options[0];
    if(selectedOption){
      this.shopParams.sort = selectedOption.value;
      this.shopParams.pageNumber = 1;
      this.getProducts();
    }
  }
  openFiltersDialog(){
    const dialogRef =  this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data:{
        selectedBrands: this.shopParams.brands,
        selectedTypes: this.shopParams.types,
      }
    });
    dialogRef.afterClosed().subscribe({
      next: result => {
        if(result){
          this.shopParams.brands = result.selectedBrands;
          this.shopParams.types = result.selectedTypes;
          this.shopParams.pageNumber = 1;
          this.getProducts();
        }
      }
    });
  }

}
