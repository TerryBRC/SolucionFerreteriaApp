﻿using DevExpress.Xpo;
using DevExpress.XtraEditors;
using FerreteriaApp.bdatosfer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FerreteriaApp.Vistas
{
    public partial class frmProveedores : MetroFramework.Forms.MetroForm
    {
        Rol rol;
        public frmProveedores(Rol rol)
        {
            InitializeComponent();
            Botones(true, false, false, false, false, false, false);
            this.rol = rol;
        }

        #region Metodos
        private void Permiso(bool p)
        {
            if (p)
            {
                if (rol.IdRol == 2)
                {
                    Botones(false, false, true, false, true, true, false);
                }
                else if (rol.IdRol == 3)
                {
                    Botones(false, false, false, false, false, false, false);
                }
                else
                {
                    Botones(false, false, true, true, true, true, true);
                }
            }else
            {
                if (rol.IdRol == 2)
                {
                    Botones(true, false, false, false, false, false, false);
                }
                else if (rol.IdRol == 3)
                {
                    Botones(false, false, false, false, false, false, false);
                }
                else
                {
                    Botones(true, false, false, false, false, false, false);
                }
            }
        }
        private void Limpiar()
        {
            teDocumento.Clear();
            teRazonSocial.Clear();
            teCorreo.Clear();
            teTel.Clear();
            teDocumento.Focus();
        }
        private void Botones(bool nuevo, bool guardar, bool actualizar, bool eliminar, bool cancelar, bool campos,bool estado)
        {
            sbNuevo.Enabled = nuevo;
            sbGuardar.Enabled = guardar;
            sbActualizar.Enabled = actualizar;
            sbEliminar.Enabled = eliminar;
            sbCancelar.Enabled = cancelar;
            teDocumento.Enabled = campos;
            teCorreo.Enabled = campos;
            teRazonSocial.Enabled = campos;
            teTel.Enabled = campos;
            sbCambiarEstado.Enabled = estado;
        }
        #endregion

        private void sbNuevo_Click(object sender, EventArgs e)
        {
            Limpiar();
            Botones(false, true, false, false, true, true,false);
        }

        private void sbCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
            Botones(true, false, false, false, false, false, false);
        }

        private void sbCambiarEstado_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRow() == null ||
                string.IsNullOrEmpty(teDocumento.Text) ||
                string.IsNullOrEmpty(teCorreo.Text) ||
                string.IsNullOrEmpty(teRazonSocial.Text) ||
                string.IsNullOrEmpty(teTel.Text)
                )
            {
                MessageBox.Show("Seleccione un registro", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Proveedor Seleccionado = (Proveedor)gridView1.GetFocusedRow();
            try
            {
                if (Seleccionado.Estado == 1)
                {
                    Seleccionado.Estado = 0;
                }
                else
                {
                    Seleccionado.Estado = 1;
                }
                Seleccionado.Save();
                unitOfWork1.CommitChanges();
                Limpiar();
                xpCollection1.Reload();
                Botones(true, false, false, false, false, false,false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                teDocumento.Text = gridView1.GetFocusedRowCellValue("Documento").ToString();
                teRazonSocial.Text = gridView1.GetFocusedRowCellValue("RazonSocial").ToString();
                teTel.Text = gridView1.GetFocusedRowCellValue("Telefono").ToString();
                teCorreo.Text = gridView1.GetFocusedRowCellValue("Correo").ToString();
                Permiso(true);
            }
        }

        private void sbEliminar_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRow() == null ||
                string.IsNullOrEmpty(teDocumento.Text) ||
                string.IsNullOrEmpty(teCorreo.Text) ||
                string.IsNullOrEmpty(teRazonSocial.Text) ||
                string.IsNullOrEmpty(teTel.Text))
            {
                MessageBox.Show("Seleccione un registro", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Proveedor Seleccionado = (Proveedor)gridView1.GetFocusedRow();
            try
            {
                Seleccionado.Delete();
                unitOfWork1.CommitChanges();
                Limpiar();
                xpCollection1.Reload();
                Botones(true, false, false, false, false, false, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void sbActualizar_Click(object sender, EventArgs e)
        {
            if (gridView1.GetFocusedRow() == null ||
                string.IsNullOrEmpty(teDocumento.Text) ||
                string.IsNullOrEmpty(teCorreo.Text) ||
                string.IsNullOrEmpty(teRazonSocial.Text) ||
                string.IsNullOrEmpty(teTel.Text)
                )
            {
                MessageBox.Show("Seleccione un registro", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Proveedor Seleccionado = (Proveedor)gridView1.GetFocusedRow();
            try
            {
                Seleccionado.Documento = teDocumento.Text;
                Seleccionado.RazonSocial = teRazonSocial.Text;
                Seleccionado.Correo = teCorreo.Text;
                Seleccionado.Telefono = teTel.Text;
                Seleccionado.Save();
                unitOfWork1.CommitChanges();
                Limpiar();
                xpCollection1.Reload();
                Botones(true, false, false, false, false, false,false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void sbGuardar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(teDocumento.Text) &&
                string.IsNullOrEmpty(teCorreo.Text) &&
                string.IsNullOrEmpty(teRazonSocial.Text) &&
                string.IsNullOrEmpty(teTel.Text))
            {
                MessageBox.Show("Campos Requeridos", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                Proveedor nuevo = new Proveedor(unitOfWork1)
                {
                    RazonSocial = teRazonSocial.Text,
                    Correo = teCorreo.Text,
                    Telefono = teTel.Text,
                    Documento = teDocumento.Text,
                    Estado = 1
                };
                nuevo.Save();
                unitOfWork1.CommitChanges();
                Limpiar();
                xpCollection1.Reload();
                Botones(true, false, false, false, false, false, false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void frmProveedores_Load(object sender, EventArgs e)
        {
            Permiso(false);
        }
    }
}