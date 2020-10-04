import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { NavBarComponent } from './nav-bar/nav-bar.component';

@NgModule({
  declarations: [NavBarComponent],
  imports: [CommonModule, RouterModule],
  exports: [NavBarComponent],
})
export class CoreModule {}
