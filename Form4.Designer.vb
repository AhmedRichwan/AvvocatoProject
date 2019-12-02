<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AddAvvocato
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(AddAvvocato))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.NewAvvocatoname = New System.Windows.Forms.TextBox()
        Me.avcadded = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(157, 93)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(159, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "تأكيد اضافة محامي جديد"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(253, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "اسم المحامي الجديد"
        '
        'NewAvvocatoname
        '
        Me.NewAvvocatoname.Location = New System.Drawing.Point(73, 37)
        Me.NewAvvocatoname.Name = "NewAvvocatoname"
        Me.NewAvvocatoname.Size = New System.Drawing.Size(174, 20)
        Me.NewAvvocatoname.TabIndex = 2
        '
        'avcadded
        '
        Me.avcadded.AutoSize = True
        Me.avcadded.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.avcadded.ForeColor = System.Drawing.Color.Green
        Me.avcadded.Location = New System.Drawing.Point(29, 98)
        Me.avcadded.Name = "avcadded"
        Me.avcadded.Size = New System.Drawing.Size(71, 13)
        Me.avcadded.TabIndex = 3
        Me.avcadded.Text = "تمت الاضافة"
        Me.avcadded.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.PictureBox1.Image = Global.Avvocato.My.Resources.Resources.Picture51
        Me.PictureBox1.Location = New System.Drawing.Point(6, 22)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(61, 49)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 29
        Me.PictureBox1.TabStop = False
        '
        'AddAvvocato
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(383, 128)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.avcadded)
        Me.Controls.Add(Me.NewAvvocatoname)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(399, 167)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(399, 167)
        Me.Name = "AddAvvocato"
        Me.Text = "اضافة محامي جديد"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents NewAvvocatoname As TextBox
    Friend WithEvents avcadded As Label
    Friend WithEvents PictureBox1 As PictureBox
End Class
