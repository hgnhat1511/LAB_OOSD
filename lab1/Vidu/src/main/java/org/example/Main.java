package org.example;

public class Main {

    public static void main(String[] args) {

        // ===== TAM GIÁC =====//
        Diem p1 = new Diem(0, 0);
        Diem p2 = new Diem(4, 0);
        Diem p3 = new Diem(0, 3);

        TamGiac tamGiac = new TamGiac(p1, p2, p3);

        System.out.println("=== TAM GIÁC ===");
        System.out.println("Diện tích: " + tamGiac.dienTich());
        System.out.println("Chu vi: " + tamGiac.chuVi());
        tamGiac.ve();


        // ===== TỨ GIÁC =====
        Diem p4 = new Diem(0, 0);
        Diem p5 = new Diem(4, 0);
        Diem p6 = new Diem(4, 3);
        Diem p7 = new Diem(0, 3);

        CTuGiac tuGiac = new CTuGiac(p4, p5, p6, p7);

        System.out.println("\n=== TỨ GIÁC ===");
        System.out.println("Diện tích: " + tuGiac.dienTich());
        System.out.println("Chu vi: " + tuGiac.chuVi());
        tuGiac.ve();


        // ===== ELLIPSE =====
        Diem tam = new Diem(0, 0);

        CEllipse ellipse = new CEllipse(tam, 5, 3);

        System.out.println("\n=== ELLIPSE ===");
        System.out.println("Diện tích: " + ellipse.dienTich());
        System.out.println("Chu vi: " + ellipse.chuVi());
        ellipse.ve();
    }
}