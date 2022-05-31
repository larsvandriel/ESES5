import { Component, OnInit } from '@angular/core';
import {Product} from '../product.model';
import {ProductService} from '../product.service';
import {OrderService} from '../order.service';
import {Router} from '@angular/router';
import {Order} from '../order.model';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  private products: Array<Product> = [];
  private contentLoaded = false;
  private routerLinkDisabled = false;

  constructor(private productService: ProductService, private orderService: OrderService,
              private router: Router) { }

  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(products => {
      products.forEach(product => {
        this.products.push(
          new Product(product.id, product.name, product.amountInStorage)
        );
      });
      this.contentLoaded = true;
    });
  }

  getProducts(): Product[]{
    return this.products;
  }

  getContentLoaded(): boolean {
    return this.contentLoaded;
  }

  deleteProduct(productId: string): void {
    this.setRouterLinkDisabled(true);
    this.productService.deleteProduct(productId).subscribe(_ => {
      window.location.reload();
    });
  }

  orderProduct(product: Product): void {
    this.setRouterLinkDisabled(true);
    this.orderService.createOrder(new Order(undefined, product.id, undefined)).subscribe(data =>
    {
      console.log(data.status);
      window.location.reload();
    });
  }

  getRouterLinkDisabled(): boolean {
    return this.routerLinkDisabled;
  }

  setRouterLinkDisabled(disabled: boolean): void {
    this.routerLinkDisabled = disabled;
  }

  navigateToProductDetails(productId: string): void {
    if (this.getRouterLinkDisabled()) {
      return;
    }
    this.router.navigate(['product', productId]);
  }
}
