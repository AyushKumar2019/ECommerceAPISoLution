import { Component, inject } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { MatDividerModule } from '@angular/material/divider';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { MatPseudoCheckbox } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
@Component({
  standalone: true,
  imports: [MatDividerModule,
      MatSelectionList,
      MatListOption,
      FormsModule,
      MatButtonModule],
  templateUrl: './filters-dialog.component.html',
  styleUrl: './filters-dialog.component.scss'
})
export class FiltersDialogComponent {
  shopService = inject(ShopService);
  private dialogref = inject(MatDialogRef<FiltersDialogComponent>);
  data = inject(MAT_DIALOG_DATA);

  selectedBrands: string[] = this.data.selectedBrands;
  selectedTypes: string[] = this.data.selectedTypes;

  applyFilter(){
    this.dialogref.close({
      selectedBrands: this.selectedBrands,
      selectedTypes: this.selectedTypes});
  }
}
