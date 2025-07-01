import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common'; 
import { ContratoService } from '../../servicios/contrato.service';

@Component({
  selector: 'app-deuda',
  standalone: true, 
  imports: [FormsModule, CommonModule], 
  templateUrl: './deuda.component.html',
})
export class DeudaComponent {
  idSuscriptor: number | null = null;
  resultado: any;

  constructor(private contratoService: ContratoService) { }

  buscar() {
    if (this.idSuscriptor)
      this.contratoService.obtenerDeuda(this.idSuscriptor).subscribe((data) => {
        this.resultado = data;
      });
  }
}
