import { CommonModule, NgFor } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PromocionService } from '../../servicios/Promocion.service';
import { CrearPromocionDTO } from '../../modelos/CrearPromocionDTO';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-modal ',
  standalone: true,
  imports: [NgFor, CommonModule, FormsModule],
  templateUrl: './modal.component.html',
})
export class ModalComponent {
  @Input() isOpen = false;
  @Input() confirmText = '';
  @Output() close = new EventEmitter<void>();
  @Output() confirm = new EventEmitter<void>();

  numeros: number[] = Array.from({ length: 101 }, (_, i) => i);

  nuevaPromocion: CrearPromocionDTO = {
    nombre: '',
    descuentoResidencial: 0,
    descuentoEmpresarial: 0,
    aplicaNuevos: false,
    tipoPromocion: '',
    vigenciaDesde: '',
    vigenciaHasta: '',
    activa: null
  };

  constructor(private service: PromocionService) { }

  crear() {
    console.log(this.nuevaPromocion);
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


  onBackdropClick(event: MouseEvent) {
    if ((event.target as HTMLElement).classList.contains('modal')) {
      this.closeModal();
    }
  }

  closeModal() {
    this.close.emit();
  }
}
