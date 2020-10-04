import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { PaginationModule } from 'ngx-bootstrap/pagination';

import { PagerComponent } from './components/pager/pager.component';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';

@NgModule({
  declarations: [PagingHeaderComponent, PagerComponent],
  imports: [CommonModule, PaginationModule.forRoot()],
  exports: [PaginationModule, PagingHeaderComponent, PagerComponent],
})
export class SharedModule {}
