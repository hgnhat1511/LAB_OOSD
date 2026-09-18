package org.example;

public class CEllipse extends CHinhVe {

    private Diem tam;
    private float banTrucLon;
    private float banTrucNho;

    public CEllipse(Diem tam, float banTrucLon, float banTrucNho) {
        this.maLoaiHinhVe = "ELLIPSE";
        this.tam = tam;
        this.banTrucLon = banTrucLon;
        this.banTrucNho = banTrucNho;
    }

    @Override
    public float dienTich() {
        return (float) (Math.PI * banTrucLon * banTrucNho);
    }

    @Override
    public float chuVi() {

        double a = banTrucLon;
        double b = banTrucNho;

        return (float) (
                Math.PI *
                        (
                                3 * (a + b)
                                        - Math.sqrt(
                                        (3 * a + b) *
                                                (a + 3 * b)
                                )
                        )
        );
    }

    @Override
    public void ve() {
        System.out.println(
                "Vẽ ellipse tâm (" +
                        tam.getX() + ", " +
                        tam.getY() + ")"
        );
    }
}
