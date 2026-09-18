package org.example;

public class TamGiac extends CHinhVe {

    private Diem diemP1;
    private Diem diemP2;
    private Diem diemP3;

    public TamGiac(Diem diemP1, Diem diemP2, Diem diemP3) {
        this.maLoaiHinhVe = "TG";
        this.diemP1 = diemP1;
        this.diemP2 = diemP2;
        this.diemP3 = diemP3;
    }

    private float khoangCach(Diem p1, Diem p2) {
        float dx = p2.getX() - p1.getX();
        float dy = p2.getY() - p1.getY();

        return (float) Math.sqrt(dx * dx + dy * dy);
    }

    @Override
    public float chuVi() {

        float a = khoangCach(diemP1, diemP2);
        float b = khoangCach(diemP2, diemP3);
        float c = khoangCach(diemP3, diemP1);

        return a + b + c;
    }

    @Override
    public float dienTich() {

        float x1 = diemP1.getX();
        float y1 = diemP1.getY();

        float x2 = diemP2.getX();
        float y2 = diemP2.getY();

        float x3 = diemP3.getX();
        float y3 = diemP3.getY();

        return (float) (
                Math.abs(
                        x1 * (y2 - y3)
                                + x2 * (y3 - y1)
                                + x3 * (y1 - y2)
                ) / 2
        );
    }

    @Override
    public void ve() {
        System.out.println("Ve tam giac");
    }
}