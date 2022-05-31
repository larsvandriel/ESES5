export class Product {
  id: string;
  name: string;
  amountInStorage: number;

  constructor(
    id: string,
    name: string,
    amountInStorage: number
  ) {
    this.id = id;
    this.name = name;
    this.amountInStorage = amountInStorage;
  }
}
