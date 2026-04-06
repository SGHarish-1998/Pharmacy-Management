import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Medicine, Sale } from '../models/medicine.model';

@Injectable({
  providedIn: 'root'
})
export class MedicineService {
  private apiUrl = 'https://localhost:7240/api/medicine';

  constructor(private http: HttpClient) { }

  getMedicines(): Observable<Medicine[]> {
    return this.http.get<Medicine[]>(this.apiUrl);
  }

  addMedicine(medicine: Medicine): Observable<Medicine> {
    return this.http.post<Medicine>(this.apiUrl, medicine);
  }

  searchMedicines(name: string): Observable<Medicine[]> {
    return this.http.get<Medicine[]>(`${this.apiUrl}/search?name=${name}`);
  }

  recordSale(medicineId: string, quantity: number): Observable<Sale> {
    return this.http.post<Sale>(`${this.apiUrl}/sale`, { medicineId, quantity });
  }
}
