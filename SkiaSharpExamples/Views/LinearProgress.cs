﻿using System;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SkiaSharpExamples
{
    public class LinearProgress : SKCanvasView
    {
        public static BindableProperty StartingPointProperty =
            BindableProperty.Create(nameof(StartingPoint), typeof(float), typeof(LinearProgress), default(float), 
                propertyChanged: (bindable, oldValue, newValue) => (bindable as SKCanvasView)?.InvalidateSurface());

        public float StartingPoint {
            get { return (float)GetValue(StartingPointProperty); }
            set { SetValue(StartingPointProperty, value.Clamp(0, 100)); }
        }
        
        public static BindableProperty EndingPointProperty =
            BindableProperty.Create(nameof(EndingPoint), typeof(float), typeof(LinearProgress), default(float), 
                propertyChanged: (bindable, oldValue, newValue) => (bindable as SKCanvasView)?.InvalidateSurface());

        public float EndingPoint {
            get { return (float)GetValue(EndingPointProperty); }
            set { SetValue(EndingPointProperty, value.Clamp(0, 100)); }
        }


        public static BindableProperty ProgressThicknessProperty =
            BindableProperty.Create(nameof(ProgressThickness), typeof(float), typeof(CircularProgress), 12f, 
                propertyChanged: (bindable, oldValue, newValue) => (bindable as SKCanvasView)?.InvalidateSurface());

        public float ProgressThickness {
            get { return (float)GetValue(ProgressThicknessProperty); }
            set { SetValue(ProgressThicknessProperty, value); }
        }
        
        public static BindableProperty ProgressColorProperty =
            BindableProperty.Create(nameof(ProgressColor), typeof(Color), typeof(CircularProgress), Color.Default, 
                propertyChanged: (bindable, oldValue, newValue) => (bindable as SKCanvasView)?.InvalidateSurface());

        public Color ProgressColor {
            get { return (Color)GetValue(ProgressColorProperty); }
            set { SetValue(ProgressColorProperty, value); }
        }

        public LinearProgress()
        {
            this.BackgroundColor = Xamarin.Forms.Color.Transparent;
        }
        
        protected override void OnParentSet()
        {
            base.OnParentSet();

            if (Parent != null)
                this.InvalidateSurface();
        }
        
        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            var canvas = e.Surface.Canvas;

            using (var paint = new SKPaint())
            using (var path = new SKPath()) {
            
                paint.IsAntialias = true;
                paint.StrokeCap = SKStrokeCap.Round;
                paint.Style = SKPaintStyle.Stroke;
                paint.Color = ProgressColor.ToSKColor();
                paint.StrokeWidth = ProgressThickness;

                var start = (((e.Info.Width) * (this.StartingPoint / 100)) + ProgressThickness).Clamp(ProgressThickness, e.Info.Width - ProgressThickness);
                var end =  (((e.Info.Width) * (this.EndingPoint / 100)) - ProgressThickness).Clamp(ProgressThickness, e.Info.Width - ProgressThickness);

                var verticalCenter = e.Info.Height / 2f;

                path.MoveTo(start, verticalCenter);
                path.LineTo(end, verticalCenter);

                canvas.Clear();
                canvas.DrawPath(path, paint);
            }
        }
    }
}
