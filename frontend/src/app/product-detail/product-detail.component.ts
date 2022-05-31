import { Component, OnInit } from '@angular/core';
import {Product} from '../product.model';
import {ProductService} from '../product.service';
import {ActivatedRoute, Router} from '@angular/router';
import {OrderService} from '../order.service';
import {Order} from '../order.model';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {

  private product: Product;
  private contentLoaded = false;

  constructor(private productService: ProductService, private orderService: OrderService,
              private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.productService.getProduct(id).subscribe(data => {
      console.log(data);
      this.product = new Product(data.id, data.name, data.amountInStorage);
      this.contentLoaded = true;
    });
  }

  getProduct(): Product {
    return this.product;
  }

  getContentLoaded(): boolean {
    return this.contentLoaded;
  }

  deleteProduct(productId: string): void {
    this.productService.deleteProduct(productId).subscribe((_ => {
      this.router.navigate(['person']);
    }));
  }

  orderProduct(): void {
    this.orderService.createOrder(new Order(undefined, this.product.id, undefined)).subscribe(_ => {
      window.location.reload();
    });
  }
}
