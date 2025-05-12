/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package javafxmlapplication;

import java.lang.reflect.Field;
import java.net.URL;
import java.time.LocalDate;
import java.time.temporal.ChronoUnit;
import static java.time.temporal.ChronoUnit.YEARS;
import java.util.List;
import java.util.ResourceBundle;
import javafx.application.Platform;
import javafx.beans.binding.Bindings;
import javafx.beans.binding.BooleanBinding;
import javafx.beans.property.BooleanProperty;
import javafx.beans.property.SimpleBooleanProperty;
import javafx.beans.value.ChangeListener;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.Node;
import javafx.scene.control.Button;
import javafx.scene.control.DateCell;
import javafx.scene.control.DatePicker;
import javafx.scene.control.Label;
import javafx.scene.control.PasswordField;
import javafx.scene.control.TextField;
import javafx.scene.image.ImageView;

public class FXMLRegisterController implements Initializable {

    @FXML
    private Label emailError;
    @FXML
    private TextField emailField;
 
    //properties to control valid fields values. 
    private BooleanProperty validEmail;
    private BooleanProperty validPassword;
    private BooleanProperty confirmPasswords;
    private BooleanProperty validDate;


 
    
    // listener to register on textProperty() or valueProperty()
    private ChangeListener<String> listenerEmail;
    private ChangeListener<String> listenerPassword;
    private ChangeListener<String> listenerRepeat;
    private ChangeListener<LocalDate> listenerDate;



    @FXML
    private TextField passwordField;
    @FXML
    private Label passwordError;
    @FXML
    private TextField repeatField;
    @FXML
    private Label repeatError;
    @FXML
    private Label BirthDateError;
    @FXML
    private DatePicker dateField;
    @FXML
    private Button bAcceptOnAction;
    @FXML
    private Button bCancel;
    

    
    private void checkEmail() {
        String email = emailField.getText();
//        boolean isValid = email.matches("^[A-Za-z0-9+_.-]+@(.+)$");
        boolean isValid = email.matches("^[\\w!#$%&'*+/=?`{|}~^-]+(?:\\.[\\w!#$%&'*+/=?`{|}~^-]+)*@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$");
        validEmail.set(isValid); //actualiza la property asociada
        showError(isValid, emailField, emailError); //muestra o esconde el mensaje de error
    }
    private void checkPassword(){
        String password =passwordField.getText();
        boolean isValid = password.matches("^(?=.*[0-9])(?=.*[a-zA-Z]).{8,15}$");
        validPassword.set(isValid); //actualiza la property asociada
        showError(isValid, passwordField, passwordError); 
        
    }


    
    private void showError(boolean isValid, Node field, Node errorMessage){
        errorMessage.setVisible(!isValid);
        field.setStyle(((isValid) ? "" : "-fx-background-color: #FCE5E0"));
    }
    
    private void checkPasswordsMatch() {
        boolean match = passwordField.getText().equals(repeatField.getText());
        confirmPasswords.set(match);
         showError(match, repeatField, repeatError);
     }
    
    private void checkDate(){
        LocalDate value = dateField.getValue();
        boolean isValid = value.isBefore(LocalDate.now().minus(16, YEARS));
        validDate.set(isValid);
        showError(isValid,dateField,BirthDateError);}
    
    @FXML
    private void handleBAcceptOnAction(ActionEvent event) {
    
        emailField.clear();
        passwordField.clear();
        repeatField.clear();
        dateField.setValue(null);
        
        validEmail.setValue(Boolean.FALSE);
        validPassword.setValue(Boolean.FALSE);
        validDate.setValue(Boolean.FALSE);
        confirmPasswords.setValue(Boolean.FALSE);
            }
    
    //=========================================================
    // you must initialize here all related with the object 
    @Override
    public void initialize(URL url, ResourceBundle rb) {

        validEmail = new SimpleBooleanProperty(false);
        validPassword = new SimpleBooleanProperty(false);
        confirmPasswords = new SimpleBooleanProperty(false);
        validDate = new SimpleBooleanProperty(false);

        BooleanBinding validFields = Bindings.and(validEmail,validPassword)
                .and(confirmPasswords)
                .and(validDate);
         bAcceptOnAction.disableProperty().bind(
                 Bindings.not(validFields)
         );  
         
         bCancel.setOnAction((event)->{
             bCancel.getScene().getWindow().hide();
         });

        //When the field loses focus, the field is validated. 
        emailField.focusedProperty().addListener((obs, oldVal, newVal) -> {
            if (!newVal) {
                checkEmail();
                if (!validEmail.get()) {
                    //If it is not correct, a listener is added to the text or value 
                    //so that the field is validated while it is being edited.
                    if (listenerEmail == null) {
                        listenerEmail = (a, b, c) -> checkEmail();
                        emailField.textProperty().addListener(listenerEmail);
                    }
                }
            }
        });
        
         passwordField.focusedProperty().addListener((obs, oldVal, newVal) -> {
            if (!newVal) {
                checkPassword();
                if (!validPassword.get()) {
                    //If it is not correct, a listener is added to the text or value 
                    //so that the field is validated while it is being edited.
                    if (listenerPassword == null) {
                        listenerPassword = (a, b, c) -> checkPassword();
                        passwordField.textProperty().addListener(listenerPassword);
                    }
                }
            }
        });
         
          repeatField.focusedProperty().addListener((obs, oldVal, newVal) -> {
            if (!newVal) {
                checkPasswordsMatch();
                if (!confirmPasswords.get()) {
                    //If it is not correct, a listener is added to the text or value 
                    //so that the field is validated while it is being edited.
                    if (listenerRepeat == null) {
                        listenerRepeat= (a, b, c) -> checkPasswordsMatch();
                        repeatField.textProperty().addListener(listenerRepeat);
                    }
                }
            }
        });
        
           dateField.focusedProperty().addListener((obs, oldVal, newVal) -> {
            if (!newVal) {
                checkDate();
                if (!validDate.get()) {
                    //If it is not correct, a listener is added to the text or value 
                    //so that the field is validated while it is being edited.
                    if (listenerDate == null) {
                        dateField.valueProperty().addListener(listenerDate);
                    }
                }
            }
        });
        
    }



}