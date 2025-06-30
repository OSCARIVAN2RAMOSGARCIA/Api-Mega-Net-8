export interface CrearPromocionDTO {
  nombre: string;
  descuentoResidencial: number;
  descuentoEmpresarial: number;
  aplicaNuevos: boolean;
  tipoPromocion: string;
  vigenciaDesde: string;
  vigenciaHasta: string;
  activa: boolean | null;
}
