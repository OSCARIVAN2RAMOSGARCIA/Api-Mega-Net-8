import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { PromocionService } from '../../../../servicios/Promocion.service';
import { CrearPromocionDTO } from '../../../../modelos/CrearPromocionDTO';

@Component({
  selector: 'app-crear-promocion',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './crear-promocion.component.html',
  styleUrls: ['./crear-promocion.component.css']
})
export class CrearPromocionComponent {
  nuevaPromocion: CrearPromocionDTO = {
    nombre: '',
    descuentoResidencial: 0,
    descuentoEmpresarial: 0,
    aplicaNuevos: false,
    tipoPromocion: '',
    vigenciaDesde: '',
    vigenciaHasta: '',
    activa: true
  };

  constructor(private service: PromocionService) {}

  crear() {
    this.service.crearPromocion(this.nuevaPromocion).subscribe(() => {
      alert('Promoción creada correctamente');

      // Limpiar formulario:
      this.nuevaPromocion = {
        nombre: '',
        descuentoResidencial: 0,
        descuentoEmpresarial: 0,
        aplicaNuevos: false,
        tipoPromocion: '',
        vigenciaDesde: '',
        vigenciaHasta: '',
        activa: true
      };
    });
  }
}
