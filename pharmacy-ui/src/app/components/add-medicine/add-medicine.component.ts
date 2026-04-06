import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { MedicineService } from '../../services/medicine.service';
import { Medicine } from '../../models/medicine.model';

@Component({
  selector: 'app-add-medicine',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './add-medicine.component.html',
  styleUrls: ['./add-medicine.component.css']
})
export class AddMedicineComponent {
  medicine: Medicine = {
    fullName: '',
    brand: '',
    expiryDate: '',
    notes: '',
    quantity: 0,
    price: 0
  };

  constructor(private medicineService: MedicineService, public router: Router) {}

  onSubmit(): void {
    this.medicineService.addMedicine(this.medicine).subscribe({
      next: () => {
        alert('Medicine added successfully!');
        this.router.navigate(['/']);
      },
      error: (err) => alert('Error adding medicine: ' + err.message)
    });
  }
}
