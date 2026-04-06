import { Routes } from '@angular/router';
import { MedicineListComponent } from './components/medicine-list/medicine-list.component';
import { AddMedicineComponent } from './components/add-medicine/add-medicine.component';

export const routes: Routes = [
  { path: '', component: MedicineListComponent },
  { path: 'add', component: AddMedicineComponent },
  { path: '**', redirectTo: '' }
];
