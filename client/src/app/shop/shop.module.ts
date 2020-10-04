import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ProductItemComponent } from './product-item/product-item.component';
import { ShopComponent } from './shop.component';

@NgModule({
  declarations: [ShopComponent, ProductItemComponent],
  imports: [CommonModule, SharedModule],
  exports: [ShopComponent],
})
export class ShopModule {}
