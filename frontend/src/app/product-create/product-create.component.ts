import { Component, OnInit } from '@angular/core';
import {Product} from '../product.model';
import {ProductService} from '../product.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css']
})
export class ProductCreateComponent implements OnInit {

  product = new Product(undefined, '', 1);

  submitted = false;

  constructor(private productService: ProductService, private router: Router) { }

  ngOnInit(): void {
  }

  onSubmit(): void {
    this.submitted = true;
  }

  createProduct(): void {
    this.productService.createProduct(this.product).subscribe(data => {
      console.log(data);
      this.router.navigate(['product', data.id]);
    });
  }
}
