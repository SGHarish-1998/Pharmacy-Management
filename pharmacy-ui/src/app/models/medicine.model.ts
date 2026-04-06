//step1:create a 2 models for type sfety.
export interface Medicine {
  id?: string;
  fullName: string;
  notes: string;
  expiryDate: string;
  quantity: number;
  price: number;
  brand: string;
}

export interface Sale {
  id?: string;
  medicineId: string;
  medicineName: string;
  quantity: number;
  totalPrice: number;
  saleDate: string;
}
