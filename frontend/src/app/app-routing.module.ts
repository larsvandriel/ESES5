import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {ProductListComponent} from './product-list/product-list.component';
import {ProductCreateComponent} from './product-create/product-create.component';
import {ProductDetailComponent} from './product-detail/product-detail.component';
import {ProductEditComponent} from './product-edit/product-edit.component';

const routes: Routes = [
  { path: 'product', component: ProductListComponent},
  { path: 'product/create', component: ProductCreateComponent},
  { path: 'product/:id', component: ProductDetailComponent, pathMatch: 'full'},
  { path: 'product/:id/edit', component: ProductEditComponent, pathMatch: 'full'}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
