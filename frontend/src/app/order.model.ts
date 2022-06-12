import {Product} from './product.model';

export class Order {
  id: string;
  productId: string;
  status: string;

  constructor(
    id: string,
    productId: string,
    status: string
  ) {
    this.id = id;
    this.productId = productId;
    this.status = status;
  }
}
