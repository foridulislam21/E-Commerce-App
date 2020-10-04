import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { ShopRoutingModule } from './shop-routing.module';
import { ShopComponent } from './shop.component';

@NgModule({
  declarations: [ShopComponent, ProductItemComponent, ProductDetailsComponent],
  imports: [CommonModule, SharedModule, ShopRoutingModule],
})
export class ShopModule {}
