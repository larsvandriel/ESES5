import { Component, OnInit } from '@angular/core';
import {Product} from '../product.model';
import {ProductService} from '../product.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.css']
})
export class ProductEditComponent implements OnInit {

  product: Product;

  submitted = false;
  contentLoaded = false;

  constructor(private productService: ProductService, private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.productService.getProduct(id).subscribe(data => {
      this.product = new Product(data.id, data.name, data.amountInStorage);
      this.contentLoaded = true;
    });
  }

  onSubmit(): void {
    this.submitted = true;
  }

  editProduct(): void {
    this.productService.editProduct(this.product).subscribe(_ => {
      this.router.navigate(['product', this.product.id]);
    });
  }
}
