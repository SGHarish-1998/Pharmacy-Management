import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MedicineService } from '../../services/medicine.service';
import { Medicine } from '../../models/medicine.model';

@Component({
  selector: 'app-medicine-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './medicine-list.component.html',
  styleUrls: ['./medicine-list.component.css']
})
export class MedicineListComponent implements OnInit {
  medicines: Medicine[] = [];
  filteredMedicines: Medicine[] = [];
  searchTerm: string = '';

  constructor(private medicineService: MedicineService) { }

  ngOnInit(): void {
    this.loadMedicines();
  }

  loadMedicines(): void {
    this.medicineService.getMedicines().subscribe(data => {
      this.medicines = data;
      this.applyFilter();
    });
  }

  applyFilter(): void {
    if (!this.searchTerm) {
      this.filteredMedicines = this.medicines;
    } else {
      this.filteredMedicines = this.medicines.filter(m =>
        m.fullName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        m.brand.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    }
  }

  isExpiringSoon(expiryDate: string): boolean {
    const today = new Date();
    const expiry = new Date(expiryDate);
    const diffTime = expiry.getTime() - today.getTime();
    const diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    return diffDays < 30;
  }

  isLowStock(quantity: number): boolean {
    return quantity < 10;
  }

  recordSale(medicine: Medicine): void {
    const qty = prompt(`Enter quantity to sell for ${medicine.fullName}:`, "1");
    if (qty && !isNaN(Number(qty))) {
      this.medicineService.recordSale(medicine.id!, Number(qty)).subscribe({
        next: () => {
          alert('Sale recorded successfully!');
          this.loadMedicines();
        },
        error: (err) => alert(err.error || 'Error recording sale')
      });
    }
  }
}
