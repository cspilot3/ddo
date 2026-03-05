using System.Collections.Generic;
using System.Text;

namespace  WebSantander.code
{
    public enum OptionType
    {
        /// <summary>
        /// Filter 
        /// </summary>
        F,

        /// <summary>
        /// Detail
        /// </summary>
        D,

        /// <summary>
        /// Filter_Detail
        /// </summary>
        F_D,

        /// <summary>
        /// Especial
        /// </summary>
        E
    }

    public class Option
    {
        #region Declaraciones

        public static Option add = new Option("add", "Agregar", OptionType.F_D);
        public static Option remove = new Option("remove", "Eliminar", OptionType.D);
        public static Option remove_componente = new Option("remove_componente", "Inhabilitar Componente", OptionType.D);
        public static Option anular = new Option("remove", "Anular", OptionType.D);
        public static Option draft = new Option("draft", "Guardar como borrador", OptionType.D);
        public static Option save = new Option("save", "Guardar", OptionType.D);
        public static Option refresh = new Option("refresh", "Actualizar", OptionType.D);
        public static Option print = new Option("print", "Imprimir", OptionType.D);
        public static Option copy = new Option("copy", "Copiar", OptionType.D);
        public static Option paste = new Option("paste", "Pegar", OptionType.D);
        public static Option calendar = new Option("calendar", "Cambiar fechas", OptionType.D);
        public static Option print_orden = new Option("print_orden", "Imprimir Orden con Presupuesto", OptionType.D);
        public static Option print_orden_pago = new Option("print_orden_pago", "Imprimir Orden Pago Detalle", OptionType.D);
        public static Option print_acta_obra = new Option("print_orden_pago", "Imprimir Acta Obra Detalle", OptionType.D);
        public static Option report = new Option("report", "Ver reporte", OptionType.D);
        public static Option export = new Option("export", "Exportar", OptionType.D);
        public static Option add_obra = new Option("add_obra", "Agregar obra", OptionType.E);
        public static Option find = new Option("find", "Buscar", OptionType.E);
        public static Option unlock = new Option("unlock", "Desbloquear", OptionType.E);
        public static Option add_capitulo = new Option("add_capitulo", "Agregar capítulo", OptionType.E);
        public static Option add_subcapitulo = new Option("add_subcapitulo", "Agregar sub capítulo", OptionType.E);
        public static Option add_actividad = new Option("add_actividad", "Agregar actividad", OptionType.E);
        public static Option milestone = new Option("milestone", "Crear hito de presupuesto", OptionType.E);
        public static Option update_prices = new Option("update_prices", "Actualizar precios de los componentes del presupuesto", OptionType.E);
        public static Option buyer = new Option("buyer", "Convertir en Comprador", OptionType.D);
        public static Option return_cmp = new Option("return_cmp", "devolucion componente", OptionType.D);
        public static Option separar_inmueble = new Option("separar_inmueble", "Separar Inmueble", OptionType.E);
        public static Option preseparar_inmueble = new Option("preseparar_inmueble", "Preseparar Inmueble", OptionType.E);
        public static Option cotizar_inmueble = new Option("cotizar_inmueble", "Cotizar Inmueble", OptionType.E);
        public static Option habilitar_inmueble = new Option("habilitar_inmueble", "Habilitar Inmueble", OptionType.E);
        public static Option venta_inmueble = new Option("venta_inmueble", "Continuar Venta", OptionType.E);
        public static Option cancelar_separacion = new Option("cancelar_separacion", "Desistir Inmueble", OptionType.E);
        public static Option agregar_ajuste = new Option("agregar_ajuste", "Agregar Ajuste", OptionType.D);
        public static Option report_inmuebles_vendidos = new Option("report_inmuebles_vendidos", "Reporte Inmuebles Vendidos", OptionType.D);
        public static Option orden_pago = new Option("orden_pago", "Orden de pago", OptionType.D);
        public static Option orden_pago_nueva = new Option("orden_pago_nueva", "Orden de pago Nueva", OptionType.F_D);
        public static Option edit_acta_anticipo = new Option("edit_acta_anticipo", "Editar Descripción Acta de Anticipo", OptionType.D);
        public static Option distribucion_remision = new Option("distribucion_remision", "Distribución componentes remisión ", OptionType.D);
        public static Option cerrar_orden = new Option("cerrar_orden", "Cerrar Orden", OptionType.D);
        public static Option add_unid_control = new Option("add_unid_control", "Agregar Unidad Control", OptionType.D);
        public static Option add_seccion = new Option("add_seccion", "Agregar Nueva Sección", OptionType.D);
        public static Option aut_percent = new Option("aut_percent", "Cambiar el porcentaje de autorización", OptionType.D);
        public static Option report_res_ord = new Option("report_res_ord", "Reporte Resumido", OptionType.F_D);
        public static Option buscar_ordenes = new Option("find_ordenes", "Buscar órdenes de pedido o de alquiler", OptionType.D);
        public static Option report_comisiones = new Option("report_comisiones", "Informe Comisiones", OptionType.E);
        public static Option lista_precios_inmuebles = new Option("lista_precios_inmuebles", "Lista precios inmuebles", OptionType.E);
        public static Option cerrar_contrato = new Option("cerrar_contrato", "Cerrar o anular contrato", OptionType.D);
        public static Option add_anticipo = new Option("add_anticipo", "Agregar anticipo", OptionType.D);
        public static Option print_minuta = new Option("print_minuta", "Imprimir minuta contrato", OptionType.D);
        public static Option increment_value = new Option("increment_value", "Aumentar el valor del presupuesto en porcentaje", OptionType.D);
        public static Option config = new Option("config", "Configurar Autorizaciones", OptionType.D);
        public static Option upd_saldo_credito = new Option("upd_saldo_credito", "Actualizar Datos", OptionType.E);
        #endregion

        #region Propiedades

        public string Name { get; private set; }
        public string Description { get; private set; }
        public OptionType OptionType { get; private set; }

        #endregion

        #region Constructores

        private Option(string nName, string nDescription, OptionType nOptionType)
        {
            Name = nName;
            Description = nDescription;
            OptionType = nOptionType;
        }

        #endregion

        #region Funciones

        public static Option Parse(string nName)
        {

            var f = typeof (Option).GetField(nName);
            return (Option) f.GetValue(new Option("", "", OptionType.D));
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }

    public class EditOptions : List<Option>
    {
        #region Metodos

        public void Add(params Option[] nOptions)
        {
            base.AddRange(nOptions);
        }

        #endregion

        #region Funciones

        public string GetJson()
        {
            var sb = new StringBuilder();
            foreach (var o in this)
            {
                if (sb.Length > 0) sb.Append(",");
                sb.Append("'" + o.Name + "-" + o.Description + "-" + o.OptionType.ToString().Replace("_", " ") + "'");
            }
            return "[" + sb + "]";
        }

        #endregion
    }
}
