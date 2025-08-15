using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MDJBussines.Helpers;
    public static class ObjectCleaner
    {
        public static void LimpiarRelacionesCirculares<T>(this IEnumerable<T> lista, int nivelMaximo = 2, int nivelActual = 0)
        {
            if (nivelActual >= nivelMaximo || lista == null)
                return;

            foreach (var item in lista)
            {
                if (item == null) continue;

                var propiedades = item.GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p =>
                        p.CanRead && p.CanWrite &&
                        !p.PropertyType.IsPrimitive &&
                        p.PropertyType != typeof(string) &&
                        !p.PropertyType.IsEnum &&
                        !p.PropertyType.IsValueType);

                foreach (var prop in propiedades)
                {
                    var valor = prop.GetValue(item);
                    if (valor == null) continue;

                    bool esColeccion = typeof(IEnumerable).IsAssignableFrom(prop.PropertyType) && prop.PropertyType != typeof(string);

                    if (esColeccion)
                    {
                        if (nivelActual + 1 >= nivelMaximo)
                        {
                            // Demasiado profundo, limpiamos
                            prop.SetValue(item, null);
                        }
                        else
                        {
                            // Profundidad aceptable, seguimos limpiando dentro de la colección
                            if (valor is IEnumerable enumerable)
                            {
                                foreach (var subItem in enumerable)
                                {
                                    LimpiarRelacionesCirculares(new[] { subItem }, nivelMaximo, nivelActual + 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        // Si es una propiedad compleja individual (no colección), continuar limpiando
                        LimpiarRelacionesCirculares(new[] { valor }, nivelMaximo, nivelActual + 1);
                    }
                }
            }
        }
    }
