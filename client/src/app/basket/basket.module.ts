import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { BasketRoutingModule } from '../basket/basket-routing.module';
import { BasketComponent } from './basket.component';



@NgModule({
  declarations: [BasketComponent],
  imports: [
    CommonModule,
    BasketRoutingModule
  ]
})
export class BasketModule { }
