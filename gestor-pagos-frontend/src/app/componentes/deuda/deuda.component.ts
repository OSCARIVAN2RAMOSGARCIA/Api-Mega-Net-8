import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; // ✅ Importa esto para *ngIf y *ngFor
import { ContratoService } from '../../servicios/contrato.service';

@Component({
  selector: 'app-deuda',
  standalone: true, // ✅Es un componente standalone
  imports: [FormsModule, CommonModule], // ✅ Necesario para ngModel, *ngIf y *ngFor
  templateUrl: './deuda.component.html',
  styleUrls: ['./deuda.component.css']
})
export class DeudaComponent {
  idSuscriptor: number = 0;
  resultado: any;

  constructor(private contratoService: ContratoService) {}

  buscar() {
    this.contratoService.obtenerDeuda(this.idSuscriptor).subscribe((data) => {
      this.resultado = data;
    });
  }
}
