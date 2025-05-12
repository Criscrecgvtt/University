/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package javafxmlapplication;

import java.net.URL;
import java.util.ResourceBundle;
import javafx.fxml.FXML;
import javafx.application.Application;
import javafx.fxml.Initializable;
import javafx.scene.control.ColorPicker;
import javafx.scene.control.Label;
import javafx.scene.control.Slider;
import javafx.scene.control.ToggleButton;
import javafx.scene.input.KeyCode;
import javafx.scene.input.KeyEvent;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.GridPane;
import javafx.scene.paint.Color;
import javafx.scene.shape.Circle;
import static javafxmlapplication.Utils.*;

/**
 *
 * @author jsoler
 */
public class FXMLDocumentController implements Initializable {
    private Label labelMessage;
    @FXML
    private Circle Circle;
    @FXML
    private GridPane gridPane;
     @FXML
   private ToggleButton myButton;
     
    private double dragStartX;
    private double dragStartY;
     private int rowCount =5;
    private int columCount = 5;
    private final double RADIO_MIN =5;
    private final double RADIO_MAX = 40;
    @FXML
    private Slider slider;
    @FXML
    private ColorPicker colorPicker;
   
   
    
    // you must initialize here all related with the object 
    @Override
    public void initialize(URL url, ResourceBundle rb) {  
        Circle.setFocusTraversable(true);
        myButton.setOnAction(event -> {
            if (myButton.isSelected()){
                Circle.setFill(Color.TRANSPARENT);
                Circle.setStroke(colorPicker.getValue());
            }
            else{
                Circle.setFill(colorPicker.getValue());
                Circle.setStroke(Color.TRANSPARENT);
            }
            Circle.requestFocus();
        
        });
        slider.setMin(RADIO_MIN);
        slider.setMax(RADIO_MAX);
        slider.setValue(25.0); // Valor inicial
        
        Circle.radiusProperty().bind(slider.valueProperty());
        
        colorPicker.setOnAction(event -> {
            Color selectedColor = colorPicker.getValue();
            Circle.setFill(selectedColor);
            Circle.requestFocus();
        
        });
        slider.valueProperty().addListener((obs, oldVal, newVal) -> {
            Circle.requestFocus();
            });
        


    }
   
    
    
    private int normalize(int num,int limit){
        return (num+limit)%limit;
    }
    @FXML
    public void handleMouseClicked(MouseEvent event){
    
    double x = event.getSceneX();
    double y = event.getSceneY();
    int col = columnCalc(gridPane,x);
     int row= rowCalc(gridPane,y);
     GridPane.setColumnIndex(Circle, col);
     GridPane.setRowIndex(Circle, row);
    
    
    
    }
    @FXML
    public void handleKeyPressed(KeyEvent event) {
        if (event.getCode() == KeyCode.UP) {
            System.out.println("moving up");
            int row = GridPane.getRowIndex(Circle);
            int newRow = normalize(row - 1, rowCount);
            GridPane.setRowIndex(Circle, newRow);
             event.consume();

        }
         if (event.getCode() == KeyCode.DOWN) {
            System.out.println("moving down");
            int row = GridPane.getRowIndex(Circle);
            int newRow = normalize(row + 1, rowCount);
            GridPane.setRowIndex(Circle, newRow);
            event.consume();

        }
         if (event.getCode() == KeyCode.RIGHT) {
            System.out.println("moving right");
            int col = GridPane.getColumnIndex(Circle);
            int newCol = normalize(col + 1, columCount);
            GridPane.setColumnIndex(Circle, newCol);
             event.consume();
    }
             if (event.getCode() == KeyCode.LEFT) {
            System.out.println("moving left");
            int col = GridPane.getColumnIndex(Circle);
            int newCol = normalize(col - 1, columCount);
            GridPane.setColumnIndex(Circle, newCol);
             event.consume();
        }
          
    }

    @FXML
    private void handleMouseDraggedBall(MouseEvent event) {
    double newX = event.getSceneX() - dragStartX;
    double newY = event.getSceneY() - dragStartY;

    Circle.setTranslateX(newX);
    Circle.setTranslateY(newY);
    }

    @FXML
    private void handleMousePressedBall(MouseEvent event) {
    
    dragStartX = event.getSceneX() ;
    dragStartY = event.getSceneY();
    }

    @FXML
    private void handleMouseReleasedBall(MouseEvent event) {
       
        // Importante: resetear LayoutX y LayoutY
        Circle.setTranslateX(0);
        Circle.setTranslateY(0);
        
        event.consume();
    }

    @FXML
    private void handleButtonClicked(MouseEvent event) {
    }
    
    }

    
   
    

