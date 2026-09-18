package org.example;

public class CTuGiac extends CHinhVe {

    private Diem diemP1;
    private Diem diemP2;
    private Diem diemP3;
    private Diem diemP4;

    public CTuGiac(Diem diemP1, Diem diemP2, Diem diemP3, Diem diemP4) {
        this.maLoaiHinhVe = "TG4";
        this.diemP1 = diemP1;
        this.diemP2 = diemP2;
        this.diemP3 = diemP3;
        this.diemP4 = diemP4;
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
        float c = khoangCach(diemP3, diemP4);
        float d = khoangCach(diemP4, diemP1);

        return a + b + c + d;
    }

    @Override
    public float dienTich() {

        float x1 = diemP1.getX();
        float y1 = diemP1.getY();

        float x2 = diemP2.getX();
        float y2 = diemP2.getY();

        float x3 = diemP3.getX();
        float y3 = diemP3.getY();

        float x4 = diemP4.getX();
        float y4 = diemP4.getY();

        float tong1 = x1 * y2 + x2 * y3 + x3 * y4 + x4 * y1;
        float tong2 = y1 * x2 + y2 * x3 + y3 * x4 + y4 * x1;

        return Math.abs(tong1 - tong2) / 2;
    }

    @Override
    public void ve() {
        System.out.println("Vẽ tứ giác qua 4 điểm P1, P2, P3, P4");
    }
}