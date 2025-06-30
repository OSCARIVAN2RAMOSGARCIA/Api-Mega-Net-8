export interface PromocionDetalleDTO {
  nombre: string;
  descuentoAplicado: number;
  fechaInicio: string;
  fechaFin: string;
  diasDuracion: number;
}

export interface ContratoDeudaDetalleDTO {
  idContrato: number;
  paquete: string;
  tipoContrato: string;
  precioOriginal: number;
  descuentoTotal: number;
  totalAPagar: number;
  promociones: PromocionDetalleDTO[];
}

export interface SuscriptorDeudaDTO {
  idSuscriptor: number;
  nombre: string;
  colonia: string;
  ciudad: string;
  deudaTotal: number;
  contratos: ContratoDeudaDetalleDTO[];
}
