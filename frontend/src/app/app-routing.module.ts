import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule, Routes} from '@angular/router';
import {ProductListComponent} from './product-list/product-list.component';
import {ProductCreateComponent} from './product-create/product-create.component';
import {ProductDetailComponent} from './product-detail/product-detail.component';
import {ProductEditComponent} from './product-edit/product-edit.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuardService as AuthGuard } from './auth-guard.service';

const routes: Routes = [
  { path: 'product', component: ProductListComponent, canActivate: [AuthGuard]},
  { path: 'product/create', component: ProductCreateComponent, canActivate: [AuthGuard]},
  { path: 'product/:id', component: ProductDetailComponent, pathMatch: 'full', canActivate: [AuthGuard]},
  { path: 'product/:id/edit', component: ProductEditComponent, pathMatch: 'full', canActivate: [AuthGuard]},
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: '**', redirectTo: 'login'}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule]
})
export class AppRoutingModule { }
