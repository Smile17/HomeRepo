var canvas = document.getElementById('canv');
var ctx = canvas.getContext('2d');

// Сітка, надписи
ctx.lineWidth="1";
	ctx.strokeStyle="lightblue";
	ctx.font="10px Arial";

	var step=100;

	for (x=0; x<800; x=x+step){
	ctx.moveTo(x,0);
	ctx.lineTo(x,600);
	ctx.stroke();
	ctx.fillText(x, x, 10);
	}
for (y=0; y<600; y=y+step){
	ctx.moveTo(0,y);
	ctx.lineTo(800,y);
	ctx.stroke();
	ctx.fillText(y, 2, y-2);
	}
pi=Math.PI;


ctx.font = "italic 26px Arial";
ctx.fillStyle = "blue";
ctx.fillText("ЗНО-2019. Математика.", 300, 50);
ctx.fillText ( "Cтатистичні дані за регіонами", 270, 80); 


//Стовбці

ctx.fillStyle = "#1636d5"; //колір першого стовбця (Синій)
ctx.fillRect (120, 200-14, 60, 500-(200-14)); //координати, ширина, висота
ctx.fill();// малюємо


ctx.fillStyle = "#1636d5";//колір стовбця(синій)
ctx.fillRect (200, 214, 60, 500-214);//координати, ширина, висота
ctx.fill(); // малюємо


ctx.fillStyle = "#1636d5"; //колір стовбця(синій)
ctx.fillRect (280, 198, 60, 500-198); //координати, ширина, висота
ctx.fill();// малюємо


ctx.fillStyle = "#1636d5"; //колір стовбця(синій)
ctx.fillRect (360, 222, 60, 500-222); //координати, ширина, висота
ctx.fill(); // малюємо

ctx.fillStyle = "#1636d5"; //колір стовбця(синій)
ctx.fillRect (440, 212, 60, 500-212); //координати, ширина, висота
ctx.fill(); // малюємо

ctx.fillStyle = "#1636d5"; //колір стовбця(синій)
ctx.fillRect (520, 220, 60, 500-220); //координати, ширина, висота
ctx.fill(); // малюємо

ctx.fillStyle = "#bf1808"; //колір останнього стовбця(Червоний)
ctx.fillRect (600, 208, 60, 500-208); //координати, ширина, висота
ctx.fill(); // малюємо


// Підписи під слайдом
ctx.fillStyle = "blue";
ctx.font = "14px Arial"; //Шрифт надпису і розмір

ctx.fillText("Київ", 120, 520); //Текст и координати
ctx.fillText("Вінн.обл.",200, 520); //Текст и координати
ctx.fillText("Львів.обл.", 280, 520); //Текст и координати
ctx.fillText("Луг.обл.", 360, 520); //Текст и координати
ctx.fillText("Київ.обл.", 440, 520); //Текст и координати
ctx.fillText("Мик.обл.", 520, 520); //Текст и координати
ctx.fillText("УКРАЇНА", 600, 520); //Текст и координати

// Цифри над стовбцями
var x = 15;
var y = 10;
ctx.fillText("157", 120+x, 200-14-y); //Текст и координати
ctx.fillText("143",200+x, 214-y); //Текст и координати
ctx.fillText("151", 280+x, 198-y); //Текст и координати
ctx.fillText("139", 360+x, 222-y); //Текст и координати
ctx.fillText("144", 440+x, 212-y); //Текст и координати
ctx.fillText("140", 520+x, 220-y); //Текст и координати
ctx.fillText("146", 600+x, 208-y); //Текст и координати

//Підписи вісей
ctx.font = "20px Arial";
ctx.fillStyle = "black"; 
ctx.fillText("Середній бал.", 50, 40);
ctx.fillText  ("Регіон",700, 530); //Текст и координати


// Координатні вісі 

ctx.beginPath();  // починаємо 
ctx.strokeStyle = "#0f0804"; // колір лінії
ctx.lineWidth = "2";  // жирність лінії
ctx.moveTo(100, 50); // перша точка
ctx.lineTo(100, 500); // Лінія до кінцевої точки
ctx.stroke(); // малюємо лінію 

ctx.beginPath(); //починаємо
ctx.strokeStyle = "#0f0804" //колір
ctx.lineWidth = "2"; //Ширина лінії
ctx.moveTo(100, 500); // перша точка
ctx.lineTo(750, 500); // Лінія до кінцевої точки
ctx.stroke(); //малюємо лінію


// Штришки на осях
ctx.font = "14px Arial";
ctx.fillStyle = "blue";
var z = 5;
step = 50
for (x=100; x<500; x=x+step){
ctx.moveTo(100-z,x);
ctx.lineTo(100+z,x);
ctx.stroke();
ctx.fillText((250-x/2), 100-6*z, x);
}

ctx.moveTo(0,0);
ctx.lineTo(0,600);
ctx.stroke();
ctx.moveTo(0,0);
ctx.lineTo(800,0);
ctx.stroke();

ctx.moveTo(800,0);
ctx.lineTo(800,600);
ctx.stroke();
ctx.moveTo(0,600);
ctx.lineTo(800,600);
ctx.stroke();




