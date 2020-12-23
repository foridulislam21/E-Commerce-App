import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { BasketRoutingModule } from '../basket/basket-routing.module';
import { SharedModule } from '../shared/shared.module';
import { BasketComponent } from './basket.component';



@NgModule({
  declarations: [BasketComponent],
  imports: [
    CommonModule,
    BasketRoutingModule,
    SharedModule
  ]
})
export class BasketModule { }
